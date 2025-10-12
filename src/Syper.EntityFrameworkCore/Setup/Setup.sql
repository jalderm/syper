
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

ALTER TABLE "AppSets" DROP COLUMN "WorkoutExerciseId1";

CREATE TABLE debug_log (
    id SERIAL PRIMARY KEY,             -- Unique identifier for each log entry
    proc_name TEXT NOT NULL,           -- Name of the procedure or function
    param_name TEXT,                   -- Name of the parameter or variable
    param_value TEXT,                  -- Value of the parameter or variable
    message TEXT,                      -- Optional message or status info
    executed_at TIMESTAMPTZ DEFAULT NOW()  -- Timestamp of log entry
);

