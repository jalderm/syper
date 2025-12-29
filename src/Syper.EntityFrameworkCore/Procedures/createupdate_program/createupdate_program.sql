CREATE OR REPLACE PROCEDURE public.createupdate_program(IN tenant_id uuid, IN p_program_json jsonb)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_program_id uuid;
    v_scheduleday jsonb;
    v_activity jsonb;

    -- For use internally
    v_day_id uuid;
    v_activity_id uuid;
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
        FROM jsonb_array_elements(p_program_json->'ScheduleDays')
        WHERE value->>'Id' <> '00000000-0000-0000-0000-000000000000'
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
            FROM jsonb_array_elements(v_scheduleday->'Activities')
            WHERE value->>'Id' <> '00000000-0000-0000-0000-000000000000'
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
END;
$procedure$