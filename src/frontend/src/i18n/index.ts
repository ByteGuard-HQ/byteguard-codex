import { createI18n } from 'vue-i18n';
import en from './en';
import da from './da';
import { getBrowserLocale } from './getBrowserLocale';
import { normalizeToSupportedLocale } from './supportedLocales';

export type SupportedLocale = 'en' | 'da';

const DEFAULT_LOCALE: SupportedLocale = 'en';

function resolveStartingLocale(): SupportedLocale {
  const stored = localStorage.getItem('locale');
  const storedNormalized = normalizeToSupportedLocale(stored);
  if (storedNormalized) return storedNormalized;

  const browserLocale = getBrowserLocale({ countryCodeOnly: true });
  const matched = normalizeToSupportedLocale(browserLocale);
  return matched ?? DEFAULT_LOCALE;
}

export const i18n = createI18n({
  legacy: false,
  globalInjection: true,
  locale: resolveStartingLocale(),
  fallbackLocale: DEFAULT_LOCALE,
  messages: {
    en,
    da,
  },
});
