CREATE SCHEMA IF NOT EXISTS pessoa;

CREATE EXTENSION IF NOT EXISTS pgcrypto;
CREATE EXTENSION IF NOT EXISTS postgis;
CREATE EXTENSION IF NOT EXISTS tablefunc;

CREATE TABLE pessoa.tbl_escolaridade (
    id_escolaridade serial NOT NULL,
    nome text NOT NULL,
    CONSTRAINT "PK_tbl_escolaridade" PRIMARY KEY (id_escolaridade)
);

CREATE TABLE pessoa.tbl_tipo_endereco (
    id_tipo_endereco serial NOT NULL,
    nome text NOT NULL,
    CONSTRAINT "PK_tbl_tipo_endereco" PRIMARY KEY (id_tipo_endereco)
);

CREATE TABLE pessoa.tbl_tipo_pessoa (
    id_tipo_pessoa serial NOT NULL,
    nome text NOT NULL,
    CONSTRAINT "PK_tbl_tipo_pessoa" PRIMARY KEY (id_tipo_pessoa)
);

CREATE TABLE pessoa.tbl_tipo_telefone (
    id_tipo_telefone serial NOT NULL,
    nome text NOT NULL,
    CONSTRAINT "PK_tbl_tipo_telefone" PRIMARY KEY (id_tipo_telefone)
);

CREATE TABLE pessoa.tbl_pessoa (
    id_pessoa uuid NOT NULL DEFAULT (gen_random_uuid()),
    data_inclusao timestamp without time zone NOT NULL DEFAULT (Now()),
    data_atualizacao timestamp without time zone NULL,
    nome character varying(250) NOT NULL,
    documento character varying(20) NOT NULL,
    email text NULL,
    data_nascimento DATE NOT NULL,
    id_tipo integer NOT NULL,
    id_escolaridade integer NULL,
    razao_social text NULL,
    deficiente boolean NULL,
    estuda boolean NULL,
    rg character varying(20) NULL,
    CONSTRAINT "PK_tbl_pessoa" PRIMARY KEY (id_pessoa),
    CONSTRAINT "FK_tbl_pessoa_tbl_escolaridade_id_escolaridade" FOREIGN KEY (id_escolaridade) REFERENCES pessoa.tbl_escolaridade (id_escolaridade) ON DELETE RESTRICT,
    CONSTRAINT "FK_tbl_pessoa_tbl_tipo_pessoa_id_tipo" FOREIGN KEY (id_tipo) REFERENCES pessoa.tbl_tipo_pessoa (id_tipo_pessoa) ON DELETE CASCADE
);

CREATE TABLE pessoa.tbl_endereco (
    id_endereco uuid NOT NULL DEFAULT (gen_random_uuid()),
    data_inclusao timestamp without time zone NOT NULL DEFAULT (Now()),
    data_atualizacao timestamp without time zone NULL,
    cep character varying(10) NOT NULL,
    uf character varying(2) NOT NULL,
    cidade character varying(50) NOT NULL,
    bairro character varying(100) NOT NULL,
    id_tipo_endereco integer NULL,
    logradouro character varying(350) NOT NULL,
    numero character varying(15) NOT NULL,
    complemento character varying(150) NULL,
    id_pessoa uuid NULL,
    CONSTRAINT "PK_tbl_endereco" PRIMARY KEY (id_endereco),
    CONSTRAINT tbl_endereco_tbl_pessoa_fk FOREIGN KEY (id_pessoa) REFERENCES pessoa.tbl_pessoa (id_pessoa) ON DELETE CASCADE,
    CONSTRAINT tbl_endereco_tbl_tipo_endereco_fk FOREIGN KEY (id_tipo_endereco) REFERENCES pessoa.tbl_tipo_endereco (id_tipo_endereco) ON DELETE RESTRICT
);

CREATE TABLE pessoa.tbl_telefone (
    id_telefone uuid NOT NULL DEFAULT (gen_random_uuid()),
    data_inclusao timestamp without time zone NOT NULL DEFAULT (Now()),
    data_atualizacao timestamp without time zone NULL,
    id_tipo integer NULL,
    ddd character varying(3) NOT NULL,
    numero character varying(15) NOT NULL,
    id_pessoa uuid NULL,
    CONSTRAINT "PK_tbl_telefone" PRIMARY KEY (id_telefone),
    CONSTRAINT tbl_telefone_tbl_pessoa_fk FOREIGN KEY (id_pessoa) REFERENCES pessoa.tbl_pessoa (id_pessoa) ON DELETE CASCADE,
    CONSTRAINT tbl_telefone_tbl_tipo_telefone_fk FOREIGN KEY (id_tipo) REFERENCES pessoa.tbl_tipo_telefone (id_tipo_telefone) ON DELETE RESTRICT
);

CREATE INDEX "IX_tbl_endereco_id_pessoa" ON pessoa.tbl_endereco (id_pessoa);

CREATE INDEX "IX_tbl_endereco_id_tipo_endereco" ON pessoa.tbl_endereco (id_tipo_endereco);

CREATE INDEX "IX_tbl_pessoa_id_escolaridade" ON pessoa.tbl_pessoa (id_escolaridade);

CREATE INDEX "IX_tbl_pessoa_id_tipo" ON pessoa.tbl_pessoa (id_tipo);

CREATE INDEX "IX_tbl_telefone_id_pessoa" ON pessoa.tbl_telefone (id_pessoa);

CREATE INDEX "IX_tbl_telefone_id_tipo" ON pessoa.tbl_telefone (id_tipo);
