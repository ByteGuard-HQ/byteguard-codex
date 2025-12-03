import axios, { AxiosError, type AxiosInstance } from 'axios';

const httpClient: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 10000,
});

// REQUEST INTERCEPTOR
httpClient.interceptors.request.use(
  (config) => {
    // TODO: When auth is implemented, inject token here.
    // Example (Pinia pseudo-code):
    //
    // const authStore = useAuthStore();
    // if (authStore.accessToken) {
    //   config.headers = config.headers ?? {};
    //   config.headers.Authorization = `Bearer ${authStore.accessToken}`;
    // }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

// RESPONSE INTERCEPTOR
httpClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    // TODO: Central auth error handling here.
    // Example:
    //
    // if (error.response?.status === 401) {
    //   const authStore = useAuthStore();
    //   await authStore.handleUnauthorized();
    // }

    // For now, just bubble up the error
    return Promise.reject(error);
  },
);

export default httpClient;
