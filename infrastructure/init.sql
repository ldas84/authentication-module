-- ============================================
-- Enable UUID generation
-- ============================================
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- ============================================
-- Table: roles
-- ============================================
CREATE TABLE IF NOT EXISTS public.roles
(
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    name varchar(100) NOT NULL,
    description text,
    CONSTRAINT roles_name_key UNIQUE (name)
);

-- ============================================
-- Table: users
-- ============================================
CREATE TABLE IF NOT EXISTS public.users
(
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    username varchar(100) NOT NULL,
    email varchar(200) NOT NULL,
    password_hash text NOT NULL,
    is_active boolean NOT NULL DEFAULT true,
    created_at timestamp NOT NULL DEFAULT now(),
    updated_at timestamp,
    CONSTRAINT users_email_key UNIQUE (email),
    CONSTRAINT users_username_key UNIQUE (username)
);

-- ============================================
-- Table: user_roles
-- ============================================
CREATE TABLE IF NOT EXISTS public.user_roles
(
    user_id uuid NOT NULL,
    role_id uuid NOT NULL,
    PRIMARY KEY (user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (role_id) REFERENCES roles(id)
);

-- ============================================
-- Initial Data
-- ============================================

INSERT INTO roles (name, description)
VALUES 
('admin', 'Administrator role'),
('user', 'Standard user role')
ON CONFLICT DO NOTHING;

INSERT INTO users (username, email, password_hash)
VALUES
('admin', 'admin@demo.com', '$2a$10$8uQ0NqzZo4PMBVXgS5aX7u3rNw2KxI0a0j0pQn6n0xQmZq3p0lQy2'),
('user', 'user@demo.com', '$2a$10$3xF7Yp9uWQ8eL2tH0mC8Uu4q9sJ5vB1fH2yE7oP9rT6wM3bN1dF2')
ON CONFLICT DO NOTHING;

-- admin → admin + user
INSERT INTO user_roles (user_id, role_id)
SELECT u.id, r.id
FROM users u, roles r
WHERE u.email = 'admin@demo.com' AND r.name IN ('admin', 'user')
ON CONFLICT DO NOTHING;

-- user → user
INSERT INTO user_roles (user_id, role_id)
SELECT u.id, r.id
FROM users u, roles r
WHERE u.email = 'user@demo.com' AND r.name = 'user'
ON CONFLICT DO NOTHING;
