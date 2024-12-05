//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import { defineMessages } from 'react-intl';
import parentMessages from '../messages';

const messages = {
  ...parentMessages,
  ...defineMessages({
    CallbackTitle: { defaultMessage: 'Redirecting...' }
  })
};

export default messages;
