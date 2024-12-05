//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

/// <reference types="vite/client" />

declare module 'ress';

interface ImportMetaEnv {
  readonly VITE_MSAL_AUTHORITY: string,
  readonly VITE_MSAL_CLIENT_APP_ID: string,
  readonly VITE_MSAL_SERVER_APP_ID: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
