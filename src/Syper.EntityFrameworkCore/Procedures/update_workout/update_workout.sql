CREATE OR REPLACE PROCEDURE public.update_workout(IN tenant_id uuid, IN p_workout_json jsonb)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_workout_id uuid;
    v_section jsonb;
    v_exercise jsonb;
    v_set jsonb;

    -- For use internally
    v_section_id uuid;
    v_workoutexercise_id uuid;
BEGIN
    -- Extract workout ID
    v_workout_id := (p_workout_json->>'Id')::uuid;

    -- Update workout name
    UPDATE "AppWorkouts"
    SET "Name" = p_workout_json->>'Name'
    WHERE "Id" = v_workout_id;
    

    -- Loop through sections
    FOR v_section IN
        SELECT * FROM jsonb_array_elements(p_workout_json->'WorkoutSections')
    LOOP
        RAISE NOTICE 'Section: %', v_section->>'Id';

        v_section_id := CASE 
            WHEN v_exercise->>'Id' = '00000000-0000-0000-0000-000000000000' 
            THEN gen_random_uuid()
            ELSE (v_section->>'Id')::uuid 
        END;

        INSERT INTO "AppWorkoutSections"(
            "Id", "Title", "Colour", "ExtraProperties", "WorkoutId",
            "ConcurrencyStamp", "CreationTime", "TenantId"
        )
        VALUES (
            v_section_id,
            v_section->>'Title',
            v_section->>'Colour',
            '{}',
            v_workout_id,
            gen_random_uuid()::text,
            NOW(),
            tenant_id
        )
        ON CONFLICT ("Id") DO UPDATE
        SET "Title" = EXCLUDED."Title",
            "Colour" = EXCLUDED."Colour";


         -- Delete removed WorkoutExercises
         -- Has to happen first so we don't remove exercises that were just inserted
        UPDATE "AppWorkoutExercises"
        SET "IsDeleted" = TRUE,
            "DeletionTime" = NOW()
        WHERE "WorkoutSectionId" = v_section_id
        AND "Id" NOT IN (
            SELECT (value->>'Id')::uuid
            FROM jsonb_array_elements(v_section->'WorkoutExercises')
            WHERE value->>'Id' IS DISTINCT FROM '00000000-0000-0000-0000-000000000000'
        );

        -- Loop through exercises
        FOR v_exercise IN
            SELECT * FROM jsonb_array_elements(v_section->'WorkoutExercises')
        LOOP
            RAISE NOTICE 'Exercise: %', v_exercise->>'Id';

            v_workoutexercise_id := CASE 
                WHEN v_exercise->>'Id' = '00000000-0000-0000-0000-000000000000' 
                THEN gen_random_uuid()
                ELSE (v_exercise->>'Id')::uuid 
            END;

            INSERT INTO "AppWorkoutExercises"(
                "Id", "ExerciseId", "WorkoutSectionId", "ExtraProperties",
                "ConcurrencyStamp", "CreationTime", "TenantId"
            )
            VALUES (
                v_workoutexercise_id,
                (v_exercise->>'ExerciseId')::uuid,
                v_section_id,
                '{}',
                gen_random_uuid()::text,
                NOW(),
                tenant_id
            )
            ON CONFLICT ("Id") DO UPDATE
            SET "ExerciseId" = EXCLUDED."ExerciseId";

            -- Delete removed sets
            -- Has to happen first so we don't remove sets that were just inserted
            UPDATE "AppSets"
            SET "IsDeleted" = TRUE,
                "DeletionTime" = NOW()
            WHERE "WorkoutExerciseId" = v_workoutexercise_id
            AND "Id" NOT IN (
                SELECT (value->>'Id')::uuid
                FROM jsonb_array_elements(v_exercise->'Sets')
                WHERE value->>'Id' IS DISTINCT FROM '00000000-0000-0000-0000-000000000000'
            );

            -- Loop through sets
            FOR v_set IN
                SELECT * FROM jsonb_array_elements(v_exercise->'Sets')
            LOOP
                RAISE NOTICE 'Set: %', v_set->>'Id';

                INSERT INTO "AppSets"(
                    "Id", "WorkoutExerciseId", "Quantity", "QuantityType",
                    "Unit", "UnitType", "Rest", "ExtraProperties",
                    "ConcurrencyStamp", "CreationTime", "TenantId", "LastModificationTime"
                )
                VALUES (
                    CASE 
                      WHEN v_set->>'Id' = '00000000-0000-0000-0000-000000000000' 
                      THEN gen_random_uuid()
                      ELSE (v_set->>'Id')::uuid 
                    END,
                    (v_workoutexercise_id)::uuid,
                    (v_set->>'Quantity')::int,
                    (v_set->>'QuantityType')::int,
                    (v_set->>'Unit')::int,
                    (v_set->>'UnitType')::int,
                    CASE 
                        WHEN v_set->>'Rest' IS NULL OR v_set->>'Rest' = 'null' THEN NULL
                        ELSE (v_set->>'Rest')::interval
                    END,
                    '{}',
                    gen_random_uuid()::text,
                    NOW(),
                    tenant_id,
                    NOW()
                )
                ON CONFLICT ("Id") DO UPDATE
                SET "Quantity" = EXCLUDED."Quantity",
                    "QuantityType" = EXCLUDED."QuantityType",
                    "Unit" = EXCLUDED."Unit",
                    "UnitType" = EXCLUDED."UnitType",
                    "Rest" = EXCLUDED."Rest",
                    "LastModificationTime" = EXCLUDED."LastModificationTime";
            END LOOP;
        END LOOP;
    END LOOP;
END;
$procedure$