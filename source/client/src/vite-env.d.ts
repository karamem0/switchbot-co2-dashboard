//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

/// <reference types="vite/client" />

declare module 'ress';

interface ImportMetaEnv {
  readonly VITE_MSAL_AUTHORITY: string,
  readonly VITE_MSAL_CLIENT_ID: string,
  readonly VITE_MSAL_SCOPES: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
