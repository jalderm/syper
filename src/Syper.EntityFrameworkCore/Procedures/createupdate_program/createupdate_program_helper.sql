
BEGIN;

-- Create debug table outside the DO block (optional for error logging)
CREATE TEMP TABLE IF NOT EXISTS debug_ex(message text);

SELECT * FROM "AppScheduleDays";

DO $$
DECLARE
    v_program_id uuid;
    v_scheduleday jsonb;
    v_activity jsonb;

    -- For use internally
    v_day_id uuid;
    v_activity_id uuid;
    
    tenant_id uuid := '3a1bc556-7269-a254-d3da-1e5de700d173';
    p_program_json jsonb := '{
    "Id": "2e17b8ac-f379-4a9e-bede-cdae030c3ab4",
    "Name": "Second Program",
    "Duration": 2,
    "ShortDescription": "D",
    "Goal": "G",
    "ScheduleDays": [
        {
            "DayOffSet": 0,
            "Activities": [
                {
                    "Name": "First Workout",
                    "WorkoutSections": [],
                    "ShortDescription": "The first workout in the system!",
                    "LastModificationTime": null,
                    "LastModifierId": null,
                    "CreationTime": "2025-09-24T20:59:00.370105",
                    "CreatorId": "3a1bc556-729d-22a6-a0a3-f9de819958cd",
                    "Id": "00000000-0000-0000-0000-000000000000",
                    "WorkoutId": "3a1c90c5-3c0f-e54a-60e2-c5a8ffe7bdf3",
                    "Type": 0,
                    "ScheduleDayId": "101af956-f68a-4f42-b527-c67beb3dd2fc"
                }
            ],
            "Notes": null,
            "ProgramId": "2e17b8ac-f379-4a9e-bede-cdae030c3ab4",
            "Id": "101af956-f68a-4f42-b527-c67beb3dd2fc"
        }
    ]
}';
BEGIN

    v_program_id := CASE
        WHEN p_program_json->>'Id' = '00000000-0000-0000-0000-000000000000' 
        THEN gen_random_uuid()
        ELSE (p_program_json->>'Id')::uuid 
    END;

    INSERT INTO "AppPrograms"(
            "Id",
            "Name",
            "Duration",
            "Goal",
            "ShortDescription",
            "ExtraProperties",
            "ConcurrencyStamp",
            "CreationTime",
            "TenantId"
        )
        VALUES (
            v_program_id,
            p_program_json->>'Name',
            CAST(p_program_json->>'Duration' AS INTEGER),
            p_program_json->>'Goal',
            p_program_json->>'ShortDescription',
            '{}',
            gen_random_uuid()::text,
            NOW(),
            tenant_id
        )
        ON CONFLICT ("Id") DO UPDATE
        SET "Name" = EXCLUDED."Name",
            "Duration" = EXCLUDED."Duration",
            "Goal" = EXCLUDED."Goal",
            "ShortDescription" = EXCLUDED."ShortDescription";



    -- Delete removed ScheduleDays
    -- Has to happen first so we don't remove exercises that were just inserted
    UPDATE "AppScheduleDays"
    SET "IsDeleted" = TRUE,
        "DeletionTime" = NOW()
    WHERE "ProgramId" = v_program_id
    AND "Id" NOT IN (
        SELECT (value->>'Id')::uuid
        FROM jsonb_array_elements(v_scheduleday->'ScheduleDays')
        WHERE value->>'Id' IS DISTINCT FROM '00000000-0000-0000-0000-000000000000'
    );

    -- Loop through schedule days
    FOR v_scheduleday IN
        SELECT * FROM jsonb_array_elements(p_program_json->'ScheduleDays')
    LOOP
        RAISE NOTICE 'ScheduleDay: %', v_scheduleday->>'Id';

        v_day_id := CASE
            WHEN v_scheduleday->>'Id' = '00000000-0000-0000-0000-000000000000' 
            THEN gen_random_uuid()
            ELSE (v_scheduleday->>'Id')::uuid 
        END;

        INSERT INTO "AppScheduleDays"(
            "Id",
            "DayOffSet",
            "Notes",
            "ProgramId",
            "CreationTime",
            -- "CreatorId",
            "TenantId",
            "ExtraProperties",
            "ConcurrencyStamp"
        )
        VALUES (
            v_day_id,
            CAST(v_scheduleday->>'DayOffSet' AS INTEGER),
            v_scheduleday->>'Notes',
            v_program_id,
            NOW(),
            -- gen_random_uuid()::text,
            tenant_id,
            '{}',
            gen_random_uuid()::text
        )
        ON CONFLICT ("Id") DO UPDATE
        SET "DayOffSet" = EXCLUDED."DayOffSet",
            "Notes" = EXCLUDED."Notes";

        -- Delete removed ScheduleDays
        -- Has to happen first so we don't remove exercises that were just inserted
        UPDATE "AppScheduleActivities"
        SET "IsDeleted" = TRUE,
            "DeletionTime" = NOW()
        WHERE "ScheduleDayId" = v_day_id
        AND "Id" NOT IN (
            SELECT (value->>'Id')::uuid
            FROM jsonb_array_elements(v_activity->'Activities')
            WHERE value->>'Id' IS DISTINCT FROM '00000000-0000-0000-0000-000000000000'
        );

        -- Loop through exercises
        FOR v_activity IN
            SELECT * FROM jsonb_array_elements(v_scheduleday->'Activities')
        LOOP
            RAISE NOTICE 'Activity: %', v_activity->>'Id';

            v_activity_id := CASE
                WHEN v_activity->>'Id' = '00000000-0000-0000-0000-000000000000' 
                THEN gen_random_uuid()
                ELSE (v_activity->>'Id')::uuid 
            END;

            INSERT INTO "AppScheduleActivities"(
                "Id",
                "Type",
                "WorkoutId",
                "ScheduleDayId",
                "TenantId",
                "ExtraProperties",
                "ConcurrencyStamp",
                "CreationTime"
            )
            VALUES (
                v_activity_id,
                CAST(v_activity->>'Type' AS INTEGER),
                CAST(v_activity->>'WorkoutId' as UUID),
                v_day_id,
                tenant_id,
                '{}',
                gen_random_uuid()::text,
                NOW()
            )
            ON CONFLICT ("Id") DO UPDATE
            SET "Type" = EXCLUDED."Type",
                "WorkoutId" = EXCLUDED."WorkoutId",
                "ScheduleDayId" = EXCLUDED."ScheduleDayId";

        END LOOP;
    END LOOP;
    
    EXCEPTION WHEN OTHERS THEN
    INSERT INTO debug_ex(message) VALUES (
        COALESCE('Error: ' || SQLERRM, 'Unknown error')
    );
END;
$$ LANGUAGE plpgsql;

SELECT * FROM debug_ex;

SELECT * FROM "AppScheduleDays";

ROLLBACK;