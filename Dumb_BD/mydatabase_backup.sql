--
-- PostgreSQL database dump
--

-- Dumped from database version 17.0
-- Dumped by pg_dump version 17.0

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AddedItems; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AddedItems" (
    "Id" integer NOT NULL,
    "UserId" integer NOT NULL,
    "Title" text,
    "Description" text,
    "Price" integer,
    "Currency" integer,
    "Image" text,
    "Place" integer,
    "Health" integer,
    "Power" integer,
    "XPover" integer
);


ALTER TABLE public."AddedItems" OWNER TO postgres;

--
-- Name: AddedItems_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."AddedItems_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."AddedItems_Id_seq" OWNER TO postgres;

--
-- Name: AddedItems_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."AddedItems_Id_seq" OWNED BY public."AddedItems"."Id";


--
-- Name: ExistWaves; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ExistWaves" (
    "Id" integer NOT NULL,
    "WavesNumber" integer,
    "DurationInSeconds" integer,
    "WavesPower" integer,
    "StartWave" timestamp with time zone,
    "Passing" double precision,
    "WaveEnd" double precision,
    "WaveHealth" integer,
    "Status" integer
);


ALTER TABLE public."ExistWaves" OWNER TO postgres;

--
-- Name: ExistWaves_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."ExistWaves_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."ExistWaves_Id_seq" OWNER TO postgres;

--
-- Name: ExistWaves_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."ExistWaves_Id_seq" OWNED BY public."ExistWaves"."Id";


--
-- Name: Items; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Items" (
    "Id" integer NOT NULL,
    "UserId" integer NOT NULL,
    "Title" text,
    "Description" text,
    "Price" integer,
    "Currency" integer,
    "Image" text,
    "Place" integer,
    "Health" integer,
    "Power" integer,
    "XPower" integer
);


ALTER TABLE public."Items" OWNER TO postgres;

--
-- Name: Tables; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Tables" (
    "Id" integer NOT NULL,
    "Height" integer,
    "Width" integer,
    "PosX" real,
    "PosY" real,
    "Rotate" real
);


ALTER TABLE public."Tables" OWNER TO postgres;

--
-- Name: OurTables_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."OurTables_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."OurTables_Id_seq" OWNER TO postgres;

--
-- Name: OurTables_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."OurTables_Id_seq" OWNED BY public."Tables"."Id";


--
-- Name: UserId_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."UserId_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."UserId_Id_seq" OWNER TO postgres;

--
-- Name: UserId_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."UserId_Id_seq" OWNED BY public."Items"."Id";


--
-- Name: WavePosition; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."WavePosition" (
    "Id" integer NOT NULL,
    "X" integer NOT NULL,
    "Y" integer NOT NULL
);


ALTER TABLE public."WavePosition" OWNER TO postgres;

--
-- Name: WavePosition_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."WavePosition_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."WavePosition_Id_seq" OWNER TO postgres;

--
-- Name: WavePosition_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."WavePosition_Id_seq" OWNED BY public."WavePosition"."Id";


--
-- Name: Waves; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Waves" (
    "Id" integer NOT NULL,
    "UserID" integer NOT NULL,
    "WavesNumber" integer,
    "DurationInSeconds" integer,
    "WavesPower" integer,
    "StartWave" timestamp with time zone,
    "Passing" double precision,
    "WaveEnd" double precision,
    "WaveHealth" integer,
    "HealthLevel1" integer,
    "HealthLevel2" integer,
    "HealthLevel3" integer,
    "Status" integer
);


ALTER TABLE public."Waves" OWNER TO postgres;

--
-- Name: Waves_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Waves_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Waves_Id_seq" OWNER TO postgres;

--
-- Name: Waves_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Waves_Id_seq" OWNED BY public."Waves"."Id";


--
-- Name: AddedItems Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddedItems" ALTER COLUMN "Id" SET DEFAULT nextval('public."AddedItems_Id_seq"'::regclass);


--
-- Name: ExistWaves Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ExistWaves" ALTER COLUMN "Id" SET DEFAULT nextval('public."ExistWaves_Id_seq"'::regclass);


--
-- Name: Items Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Items" ALTER COLUMN "Id" SET DEFAULT nextval('public."UserId_Id_seq"'::regclass);


--
-- Name: Tables Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Tables" ALTER COLUMN "Id" SET DEFAULT nextval('public."OurTables_Id_seq"'::regclass);


--
-- Name: WavePosition Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."WavePosition" ALTER COLUMN "Id" SET DEFAULT nextval('public."WavePosition_Id_seq"'::regclass);


--
-- Name: Waves Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Waves" ALTER COLUMN "Id" SET DEFAULT nextval('public."Waves_Id_seq"'::regclass);


--
-- Data for Name: AddedItems; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AddedItems" ("Id", "UserId", "Title", "Description", "Price", "Currency", "Image", "Place", "Health", "Power", "XPover") FROM stdin;
125	1	string	string	100	2	string	4	35	3	0
126	1	Exempl	Exempl Description	100	0	1	5	1	1	0
41	1	string	string	100	0	string	10	15	15	0
\.


--
-- Data for Name: ExistWaves; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ExistWaves" ("Id", "WavesNumber", "DurationInSeconds", "WavesPower", "StartWave", "Passing", "WaveEnd", "WaveHealth", "Status") FROM stdin;
2	1	100	15	2024-10-29 15:13:11.386+05	0	0	110	1
3	2	110	20	2024-10-29 15:13:11.386+05	0	0	120	1
4	3	110	25	2024-10-29 15:13:11.386+05	0	0	130	1
5	4	110	30	2024-10-29 15:13:11.386+05	0	0	140	1
\.


--
-- Data for Name: Items; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Items" ("Id", "UserId", "Title", "Description", "Price", "Currency", "Image", "Place", "Health", "Power", "XPower") FROM stdin;
115	1	sts	string	0	0	string	2	0	10	0
118	1	Exempl	Exempl Description	100	0	1	5	1	1	0
119	1	sts	string	0	0	string	2	0	10	0
109	1	Exempl	Exempl Description	100	0	1	5	1	1	0
\.


--
-- Data for Name: Tables; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Tables" ("Id", "Height", "Width", "PosX", "PosY", "Rotate") FROM stdin;
1	2	2	-165	-40	-45
2	2	2	300	-35	45
\.


--
-- Data for Name: WavePosition; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."WavePosition" ("Id", "X", "Y") FROM stdin;
1	50	150
\.


--
-- Data for Name: Waves; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Waves" ("Id", "UserID", "WavesNumber", "DurationInSeconds", "WavesPower", "StartWave", "Passing", "WaveEnd", "WaveHealth", "HealthLevel1", "HealthLevel2", "HealthLevel3", "Status") FROM stdin;
8	1	4	110	30	2024-10-29 15:13:11.386+05	0	0	140	-1	-1	-1	1
6	0	0	30	1	2024-11-08 17:05:24.709621+05	0	100	100	100	100	84	4
\.


--
-- Name: AddedItems_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AddedItems_Id_seq"', 126, true);


--
-- Name: ExistWaves_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."ExistWaves_Id_seq"', 5, true);


--
-- Name: OurTables_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."OurTables_Id_seq"', 2, true);


--
-- Name: UserId_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."UserId_Id_seq"', 119, true);


--
-- Name: WavePosition_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."WavePosition_Id_seq"', 1, true);


--
-- Name: Waves_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Waves_Id_seq"', 8, true);


--
-- Name: AddedItems AddedItems_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddedItems"
    ADD CONSTRAINT "AddedItems_pkey" PRIMARY KEY ("Id");


--
-- Name: ExistWaves ExistWaves_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ExistWaves"
    ADD CONSTRAINT "ExistWaves_pkey" PRIMARY KEY ("Id");


--
-- Name: Tables OurTables_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Tables"
    ADD CONSTRAINT "OurTables_pkey" PRIMARY KEY ("Id");


--
-- Name: Items UserId_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Items"
    ADD CONSTRAINT "UserId_pkey" PRIMARY KEY ("Id");


--
-- Name: WavePosition WavePosition_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."WavePosition"
    ADD CONSTRAINT "WavePosition_pkey" PRIMARY KEY ("Id");


--
-- Name: Waves Waves_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Waves"
    ADD CONSTRAINT "Waves_pkey" PRIMARY KEY ("Id");


--
-- PostgreSQL database dump complete
--

