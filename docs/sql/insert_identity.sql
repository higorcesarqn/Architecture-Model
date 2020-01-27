INSERT INTO identidade.permission (name,title,"Description") VALUES 
('usuarios-adicionar','Adicionar Usuários','Pode Adicionar novos usuários no sistema.')
,('usuarios-editar','Editar Usuários','Pode Editar um usuário já cadastrado no sistema.')
,('usuarios-detalhar','Detalhar Usuário','Pode Detalhar as informações de um usuário.')
,('usuarios-listar','Listar Usuários','Pode Listar os usuários cadastrados no sistema.')
,('grupos-adicionar','Adicionar Grupos','Pode Adicionar novos Grupos no sistema.')
,('grupos-editar','Editar Grupos','Pode Editar um grupo cadastrado no sistema.')
,('grupos-listar','Listar Grupos','Pode Listar os grupos cadastrados no sistema.')
,('grupos-detalhar','Detalhar Grupo','Pode Detalhar as informações de um grupo.')
,('pessoas-detalhar','Detalhar Pessoa','Pode Detalhar as informações de uma pessoa.')
,('pessoas-adicionar','Adicionar Pessoa','Pode Adicionar novas pessoas no sistema.')
,('pessoas-listar','Listar Pessoas','Pode Listar as Pessoas cadastradas no sistema.')
,('pessoas-editar','Editar Pessoas','Pode Editar uma pessoa cadastrada no sistema.')
;

INSERT INTO identidade."role" (id,name,normalized_name,concurrency_stamp) VALUES 
('0da9229a-8364-470e-b75b-7bb2abb89bd8','administrador de usuários','ADMINISTRADOR DE USUÁRIOS','fdb0ba56-b8a3-4266-9400-c0cddbe732bf')
;

INSERT INTO identidade.role_claims (role_id,claim_type,claim_value) VALUES 
('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','usuarios-adicionar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','usuarios-editar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','usuarios-detalhar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','usuarios-listar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','grupos-adicionar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','grupos-editar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','grupos-listar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','grupos-detalhar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','pessoas-adicionar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','pessoas-editar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','pessoas-listar')
,('0da9229a-8364-470e-b75b-7bb2abb89bd8','permissao','pessoas-detalhar')
;

INSERT INTO identidade."user" (id,username,normalized_username,email,normalized_email,email_confirmed,password_hash,security_stamp,concurrency_stamp,phone_number,phone_number_confirmed,two_factor_enabled,lockout_end,lockout_enabled,access_failed_count,name) VALUES 
('fddbef42-d526-4b35-a52a-2f7e0c4ad243','administrador','ADMINISTRADOR','administrador@egl.com','ADMINISTRADOR@EGL.COM',false,'AQAAAAEAACcQAAAAEPmhsR821b2ICMu+rnT4dcRZdCUzDIXkwVg5HjMR6ly6fssZGC2M698/6Hk0Lu993Q==','M5ZGOSGXMIOPSGVUCF43UGK46YWWCBQH','e4d3017a-1cd3-40ba-ac4c-b4cc20a342de',NULL,false,false,NULL,true,0,'Administrador')
;

INSERT INTO identidade.user_role (user_id,role_id) VALUES 
('fddbef42-d526-4b35-a52a-2f7e0c4ad243','0da9229a-8364-470e-b75b-7bb2abb89bd8')
;