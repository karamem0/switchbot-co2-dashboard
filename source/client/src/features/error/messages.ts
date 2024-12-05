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
    Error404Description: { defaultMessage: 'We can\'t find the page you\'re looking for' },
    Error404Title: { defaultMessage: 'Page not found' },
    Error500Description: { defaultMessage: 'Please try refreshing your browser' },
    Error500Title: { defaultMessage: 'Something went wrong' }
  })
};

export default messages;
