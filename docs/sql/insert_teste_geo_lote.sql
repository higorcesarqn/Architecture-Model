﻿CREATE TABLE public.geo_lote (
	id_lote uuid NOT NULL DEFAULT gen_random_uuid(),
	codigo_planta_quadra text NULL,
	inscricao_fiscal text NULL,
	lote_projetado text NULL,
	numero_lote text NULL,
	geom geometry NULL,
	data_inclusao timestamp NOT NULL DEFAULT now(),
	data_atualizacao timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "PK_geo_lote" PRIMARY KEY (id_lote)
);

INSERT INTO public.geo_lote (id_lote,data_inclusao,data_atualizacao,codigo_planta_quadra,inscricao_fiscal,lote_projetado,numero_lote,geom) VALUES 
('53bacf2d-1364-4f5e-922e-c8e8787632a6','2019-05-08 23:00:13.278','2019-05-08 23:00:13.278','6bd9213d','67520de8','dd8b9df2-9d96','e203','SRID=31982;MULTIPOLYGON (((508411.6961 8032551.412, 508405.8817 8032563.1659, 508384.1809 8032552.0082, 508384.7463 8032550.831, 508387.3657 8032545.3779, 508389.7984 8032540.3132, 508411.6961 8032551.412)))')
,('7c912e3b-ff9f-4eee-9005-eed83f926ea5','2019-05-08 23:00:13.340','2019-05-08 23:00:13.340','61142dc0','105e7c9f','45a094c8-ad93','86d3','SRID=31982;MULTIPOLYGON (((508353.2271 8032463.0707, 508364.5927 8032468.9713, 508358.7692 8032480.5964, 508347.0666 8032474.4548, 508353.2271 8032463.0707)))')
,('5d8e5664-173a-4e8d-b02e-3306e352dad6','2019-05-08 23:00:13.343','2019-05-08 23:00:13.343','99c70d72','1b0ccc78','a729ac80-dd86','3b6c','SRID=31982;MULTIPOLYGON (((508417.6339 8032539.4086, 508411.6961 8032551.412, 508389.7984 8032540.3132, 508395.4589 8032528.5285, 508417.6339 8032539.4086)))')
,('b54ffd8d-5688-4203-9fa7-a90f36420b9a','2019-05-08 23:00:13.344','2019-05-08 23:00:13.344','fa667980','e93952cf','2cbf1090-58f2','f7e5','SRID=31982;MULTIPOLYGON (((508347.1205 8032503.85, 508341.388 8032515.2935, 508318.0757 8032503.2217, 508323.7485 8032491.8888, 508347.1205 8032503.85)))')
,('5f240692-a6a3-4201-96bd-a29f4885224e','2019-05-08 23:00:13.345','2019-05-08 23:00:13.345','5d512d7b','cbcd117d','39484de2-81f9','7e09','SRID=31982;MULTIPOLYGON (((508354.1429 8032536.5098, 508348.0711 8032533.3329, 508362.2933 8032504.3506, 508368.5804 8032507.5414, 508354.1429 8032536.5098)))')
,('be92225f-1d6f-4b11-9e7d-6d076ba2cc48','2019-05-08 23:00:13.347','2019-05-08 23:00:13.347','638eea18','72360c60','fc859433-044b','a58c','SRID=31982;MULTIPOLYGON (((508354.1429 8032536.5098, 508348.0711 8032533.3329, 508362.2933 8032504.3506, 508368.5804 8032507.5414, 508354.1429 8032536.5098)))')
,('edffa55f-0d85-4fca-a983-a85ea490d934','2019-05-08 23:00:13.348','2019-05-08 23:00:13.348','2f3deacf','92607049','ad05f985-6d3e','e8e6','SRID=31982;MULTIPOLYGON (((508398.2958 8032522.6225, 508395.4589 8032528.5285, 508389.7984 8032540.3132, 508387.3657 8032545.3779, 508384.7463 8032550.831, 508384.1809 8032552.0082, 508372.1423 8032545.8184, 508386.2977 8032516.5333, 508398.2958 8032522.6225)))')
,('74d10b7a-bbfd-439e-961d-6c07bfe63d34','2019-05-08 23:00:13.350','2019-05-08 23:00:13.350','9424edae','77366429','34accb4e-464d','6db0','SRID=31982;MULTIPOLYGON (((508400.4762 8032487.2004, 508386.2977 8032516.5333, 508378.1233 8032512.3845, 508375.2282 8032510.9152, 508374.4425 8032510.5165, 508389.2189 8032481.5459, 508400.4762 8032487.2004)))')
,('e756db7c-833a-4f2d-975d-47128da50e81','2019-05-08 23:00:13.351','2019-05-08 23:00:13.351','c91f3b23','44a1ff7a','74f983b2-4dab','b418','SRID=31982;MULTIPOLYGON (((508352.9216 8032492.2696, 508349.9961 8032498.1096, 508347.1205 8032503.85, 508323.7485 8032491.8888, 508329.664 8032480.0707, 508352.9216 8032492.2696)))')
,('86a3026a-3f21-401a-af9c-8f55abc3c81c','2019-05-08 23:00:13.353','2019-05-08 23:00:13.353','cf0a098b','cf3c7685','5f6d54d0-c5c1','5ae1','SRID=31982;MULTIPOLYGON (((508376.5966 8032475.2033, 508362.2933 8032504.3506, 508362.205 8032504.3058, 508361.9744 8032504.1888, 508349.9961 8032498.1096, 508352.9216 8032492.2696, 508358.7692 8032480.5964, 508364.5927 8032468.9713, 508376.5966 8032475.2033)))')
;