
ALTER TABLE public."LanguageTranslations"
RENAME COLUMN "LanguageWordId" TO "LanguageInitialId";

ALTER TABLE public."LanguageTranslations"
RENAME COLUMN "LanguageId" TO "LanguageToTranslateId";