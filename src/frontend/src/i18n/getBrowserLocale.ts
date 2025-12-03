export function getBrowserLocale(options: { countryCodeOnly?: boolean } = {}): string | undefined {
  const defaultOptions = { countryCodeOnly: false };
  const opt = { ...defaultOptions, ...options };

  const navigatorLocale =
    navigator.languages !== undefined && navigator.languages.length > 0 ? navigator.languages[0] : navigator.language; // primary language, e.g. "en-GB"

  if (!navigatorLocale || typeof navigatorLocale !== 'string') {
    return undefined;
  }

  const trimmed = navigatorLocale.trim();

  if (opt.countryCodeOnly) {
    return trimmed.split(/-|_/)[0]; // "en-GB" -> "en"
  }

  return trimmed;
}
