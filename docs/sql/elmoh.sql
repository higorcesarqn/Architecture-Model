CREATE TABLE public.elmah_error (
	errorid uuid NOT NULL,
	application varchar(60) NOT NULL,
	host varchar(50) NOT NULL,
	"type" varchar(100) NOT NULL,
	"source" varchar(60) NOT NULL,
	message varchar(500) NOT NULL,
	"User" varchar(50) NOT NULL,
	statuscode int4 NOT NULL,
	timeutc timestamp NOT NULL,
	"sequence" serial NOT NULL,
	allxml text NOT NULL,
	CONSTRAINT pk_elmah_error PRIMARY KEY (errorid)
);
CREATE INDEX ix_elmah_error_app_time_seq ON public.elmah_error USING btree (application, timeutc DESC, sequence DESC);