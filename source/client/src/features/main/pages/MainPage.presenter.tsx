//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import {
  Card,
  CardHeader,
  Spinner,
  Text
} from '@fluentui/react-components';
import {
  DataVizPalette,
  GaugeChart,
  getColorFromToken
} from '@fluentui/react-charts';
import { FormattedDate, FormattedMessage, useIntl } from 'react-intl';
import { Helmet, HelmetProvider } from 'react-helmet-async';
import { EventData } from '../types/Model';
import { css } from '@emotion/react';
import messages from '../messages';
import { useTheme } from '../../../providers/ThemeProvider';

interface MainPageProps {
  items?: EventData[],
  loading?: boolean
}

function MainPage(props: MainPageProps) {

  const {
    items,
    loading
  } = props;

  const intl = useIntl();
  const { theme } = useTheme();

  return (
    <React.Fragment>
      <HelmetProvider>
        <Helmet>
          <meta
            content={intl.formatMessage(messages.AppCreator)}
            name="author" />
          <meta
            content={intl.formatMessage(messages.AppDescription)}
            name="description" />
          <title>
            {intl.formatMessage(messages.AppTitle)}
          </title>
        </Helmet>
      </HelmetProvider>
      <div
        css={css`
          display: grid;
          min-height: 100svh;
          padding: 1rem;
          background-color: ${theme.colorNeutralBackground3};
      `}>
        {
          loading ? (
            <Spinner />
          ) : (
            <div
              css={css`
                display: grid;
                grid-gap: 1rem;
              `}>
              {
                items?.map((item) => (
                  <Card key={item.deviceMac}>
                    <CardHeader
                      css={css`
                        & > div {
                          display: grid;
                        }
                      `}
                      header={(
                        <div
                          css={css`
                            display: flex;
                            flex-direction: row;
                            align-items: center;
                            justify-content: space-between;
                          `}>
                          <Text weight="semibold">
                            {item.deviceName}
                          </Text>
                          <Text>
                            <FormattedMessage {...messages.LastUpdated} />
                            {': '}
                            <FormattedDate
                              {...{
                                year: 'numeric',
                                month: '2-digit',
                                day: '2-digit',
                                hour: '2-digit',
                                minute: '2-digit'
                              }}
                              value={item.lastUpdateTime} />
                          </Text>
                        </div>
                      )} />
                    <div
                      css={css`
                        display: grid;
                        place-items: center center;
                      `}>
                      <div
                        css={css`
                          display: flex;
                        `}>
                        <GaugeChart
                          chartTitle={intl.formatMessage(messages.Temperature)}
                          chartValue={item.temperature ?? 0}
                          chartValueFormat={(a) => a[0].toString()}
                          height={160}
                          hideLegend
                          hideMinMax
                          hideTooltip
                          maxValue={40}
                          minValue={0}
                          sublabel='°C'
                          width={240}
                          segments={[
                            {
                              size: 15,
                              color: getColorFromToken(DataVizPalette.success),
                              legend: intl.formatMessage(messages.Cold)
                            },
                            {
                              size: 10,
                              color: getColorFromToken(DataVizPalette.warning),
                              legend: intl.formatMessage(messages.Cool)
                            },
                            {
                              size: 15,
                              color: getColorFromToken(DataVizPalette.error),
                              legend: intl.formatMessage(messages.Warm)
                            }
                          ]} />
                        <GaugeChart
                          chartTitle={intl.formatMessage(messages.Humidity)}
                          chartValue={item.humidity ?? 0}
                          chartValueFormat={(a) => a[0].toString()}
                          height={160}
                          hideLegend
                          hideMinMax
                          hideTooltip
                          maxValue={100}
                          minValue={0}
                          sublabel='%'
                          width={240}
                          segments={[
                            {
                              size: 33,
                              color: getColorFromToken(DataVizPalette.success),
                              legend: intl.formatMessage(messages.Cold)
                            },
                            {
                              size: 34,
                              color: getColorFromToken(DataVizPalette.warning),
                              legend: intl.formatMessage(messages.Cool)
                            },
                            {
                              size: 33,
                              color: getColorFromToken(DataVizPalette.error),
                              legend: intl.formatMessage(messages.Warm)
                            }
                          ]} />
                        <GaugeChart
                          chartTitle={intl.formatMessage(messages.CO2)}
                          chartValue={item.CO2 ?? 0}
                          chartValueFormat={(a) => a[0].toString()}
                          height={160}
                          hideLegend
                          hideMinMax
                          hideTooltip
                          maxValue={100}
                          minValue={0}
                          sublabel="ppm"
                          width={240}
                          segments={[
                            {
                              size: 1000,
                              color: getColorFromToken(DataVizPalette.success),
                              legend: intl.formatMessage(messages.Cold)
                            },
                            {
                              size: 1000,
                              color: getColorFromToken(DataVizPalette.warning),
                              legend: intl.formatMessage(messages.Cool)
                            },
                            {
                              size: 1000,
                              color: getColorFromToken(DataVizPalette.error),
                              legend: intl.formatMessage(messages.Warm)
                            }
                          ]} />
                      </div>
                    </div>
                  </Card>
                ))
              }
            </div>
          )
        }
      </div>
    </React.Fragment>
  );

};

export default React.memo(MainPage);
