create table errors
(
    Id serial
        constraint errors_pk
            primary key,
    message text,
    stack_trace text,
    project_name text,
    ip_address varchar,
    created_on timestamp
);

