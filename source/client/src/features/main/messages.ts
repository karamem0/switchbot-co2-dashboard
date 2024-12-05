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
    CO2: { defaultMessage: 'CO2' },
    Cold: { defaultMessage: 'Cold' },
    Cool: { defaultMessage: 'Cool' },
    Hot: { defaultMessage: 'Hot' },
    Humidity: { defaultMessage: 'Humidity' },
    LastUpdated: { defaultMessage: 'Last Updated' },
    Temperature: { defaultMessage: 'Temperature' },
    Warm: { defaultMessage: 'Warm' }
  })
};

export default messages;
