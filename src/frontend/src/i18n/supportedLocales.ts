import type { SupportedLocale } from './index'; // "en" | "da" etc.

const supportedLocales: SupportedLocale[] = ['en', 'da'];

export function isLocaleSupported(locale: string | undefined | null): locale is SupportedLocale {
  if (!locale) return false;

  // Accept both full tags and base codes:
  // "en-GB" -> check "en"
  const base = locale.split(/-|_/)[0];

  return supportedLocales.includes(base as SupportedLocale);
}

export function normalizeToSupportedLocale(locale: string | undefined | null): SupportedLocale | null {
  if (!locale) return null;

  const base = locale.split(/-|_/)[0] as SupportedLocale;
  return isLocaleSupported(base) ? base : null;
}
