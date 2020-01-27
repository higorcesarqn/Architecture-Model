CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE TABLE permission (
    id serial NOT NULL,
    name character varying(50) NOT NULL,
    title character varying(256) NULL,
    "Description" text NULL,
    CONSTRAINT "PK_permission" PRIMARY KEY (id)
);

CREATE TABLE role (
    id uuid NOT NULL,
    name character varying(256) NULL,
    normalized_name character varying(256) NULL,
    concurrency_stamp text NULL,
    CONSTRAINT "PK_role" PRIMARY KEY (id)
);

CREATE TABLE "user" (
    id uuid NOT NULL,
    username character varying(30) NULL,
    normalized_username character varying(30) NULL,
    email character varying(256) NULL,
    normalized_email character varying(256) NULL,
    email_confirmed boolean NOT NULL,
    password_hash text NULL,
    security_stamp text NULL,
    concurrency_stamp text NULL,
    phone_number text NULL,
    phone_number_confirmed boolean NOT NULL,
    two_factor_enabled boolean NOT NULL,
    lockout_end timestamp with time zone NULL,
    lockout_enabled boolean NOT NULL,
    access_failed_count integer NOT NULL,
    name character varying(60) NULL,
    CONSTRAINT "PK_user" PRIMARY KEY (id)
);

CREATE TABLE role_claims (
    id serial NOT NULL,
    role_id uuid NOT NULL,
    claim_type text NULL,
    claim_value text NULL,
    CONSTRAINT "PK_role_claims" PRIMARY KEY (id),
    CONSTRAINT "FK_role_claims_role_role_id" FOREIGN KEY (role_id) REFERENCES role (id) ON DELETE CASCADE
);

CREATE TABLE user_claim (
    id serial NOT NULL,
    user_id uuid NOT NULL,
    claim_type text NULL,
    claim_value text NULL,
    CONSTRAINT "PK_user_claim" PRIMARY KEY (id),
    CONSTRAINT "FK_user_claim_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (id) ON DELETE CASCADE
);

CREATE TABLE user_login (
    login_provider text NOT NULL,
    provider_key text NOT NULL,
    provider_display_name text NULL,
    user_id uuid NOT NULL,
    CONSTRAINT "PK_user_login" PRIMARY KEY (login_provider, provider_key),
    CONSTRAINT "FK_user_login_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (id) ON DELETE CASCADE
);

CREATE TABLE user_role (
    user_id uuid NOT NULL,
    role_id uuid NOT NULL,
    CONSTRAINT "PK_user_role" PRIMARY KEY (user_id, role_id),
    CONSTRAINT "FK_user_role_role_role_id" FOREIGN KEY (role_id) REFERENCES role (id) ON DELETE CASCADE,
    CONSTRAINT "FK_user_role_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (id) ON DELETE CASCADE
);

CREATE TABLE user_token (
    user_id uuid NOT NULL,
    login_provider text NOT NULL,
    name text NOT NULL,
    value text NULL,
    CONSTRAINT "PK_user_token" PRIMARY KEY (user_id, login_provider, name),
    CONSTRAINT "FK_user_token_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX role_normalize_name_index ON role (normalized_name);

CREATE INDEX role_claims_id_index ON role_claims (role_id);

CREATE INDEX email_index ON "user" (normalized_email);

CREATE UNIQUE INDEX user_normalized_name_index ON "user" (normalized_username);

CREATE INDEX user_claim_id_index ON user_claim (user_id);

CREATE INDEX user_login_id_index ON user_login (user_id);

CREATE INDEX user_role_id_index ON user_role (role_id);
