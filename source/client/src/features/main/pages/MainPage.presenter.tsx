//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import { css } from '@emotion/react';
import {
  DataVizPalette,
  GaugeChart,
  getColorFromToken
} from '@fluentui/react-charts';
import {
  Card,
  CardHeader,
  Spinner,
  Text
} from '@fluentui/react-components';
import { FormattedDate, FormattedMessage, useIntl } from 'react-intl';
import { useTheme } from '../../../providers/ThemeProvider';
import messages from '../messages';
import { EventData } from '../types/Model';

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

  return (
    <React.Fragment>
      <meta
        content={intl.formatMessage(messages.AppCreator)}
        name="author" />
      <meta
        content={intl.formatMessage(messages.AppDescription)}
        name="description" />
      <title>
        {intl.formatMessage(messages.AppTitle)}
      </title>
      <div
        css={css`
          display: grid;
          grid-template-rows: 3rem auto;
          grid-template-columns: 1fr;
          min-height: 100svh;
          padding: 1rem;
      `}>
        {
          loading ? (
            <Spinner />
          ) : (
            <React.Fragment>
              <h1>
                {intl.formatMessage(messages.AppTitle)}
              </h1>
              <div
                css={css`
                  display: grid;
                  gap: 1rem;
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
                                  day: '2-digit',
                                  hour: '2-digit',
                                  minute: '2-digit',
                                  month: '2-digit',
                                  year: 'numeric'
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
                            hideLegend
                            hideMinMax
                            hideTooltip
                            chartTitle={intl.formatMessage(messages.Temperature)}
                            chartValue={item.temperature ?? 0}
                            chartValueFormat={(a) => a[0].toString()}
                            height={160}
                            maxValue={40}
                            minValue={0}
                            sublabel='°C'
                            width={240}
                            segments={[
                              {
                                color: getColorFromToken(DataVizPalette.success),
                                legend: intl.formatMessage(messages.Cold),
                                size: 15
                              },
                              {
                                color: getColorFromToken(DataVizPalette.warning),
                                legend: intl.formatMessage(messages.Cool),
                                size: 10
                              },
                              {
                                color: getColorFromToken(DataVizPalette.error),
                                legend: intl.formatMessage(messages.Warm),
                                size: 15
                              }
                            ]} />
                          <GaugeChart
                            hideLegend
                            hideMinMax
                            hideTooltip
                            chartTitle={intl.formatMessage(messages.Humidity)}
                            chartValue={item.humidity ?? 0}
                            chartValueFormat={(a) => a[0].toString()}
                            height={160}
                            maxValue={100}
                            minValue={0}
                            sublabel='%'
                            width={240}
                            segments={[
                              {
                                color: getColorFromToken(DataVizPalette.success),
                                legend: intl.formatMessage(messages.Cold),
                                size: 33
                              },
                              {
                                color: getColorFromToken(DataVizPalette.warning),
                                legend: intl.formatMessage(messages.Cool),
                                size: 34
                              },
                              {
                                color: getColorFromToken(DataVizPalette.error),
                                legend: intl.formatMessage(messages.Warm),
                                size: 33
                              }
                            ]} />
                          <GaugeChart
                            hideLegend
                            hideMinMax
                            hideTooltip
                            chartTitle={intl.formatMessage(messages.CO2)}
                            chartValue={item.CO2 ?? 0}
                            chartValueFormat={(a) => a[0].toString()}
                            height={160}
                            maxValue={100}
                            minValue={0}
                            sublabel="ppm"
                            width={240}
                            segments={[
                              {
                                color: getColorFromToken(DataVizPalette.success),
                                legend: intl.formatMessage(messages.Cold),
                                size: 1000
                              },
                              {
                                color: getColorFromToken(DataVizPalette.warning),
                                legend: intl.formatMessage(messages.Cool),
                                size: 1000
                              },
                              {
                                color: getColorFromToken(DataVizPalette.error),
                                legend: intl.formatMessage(messages.Warm),
                                size: 1000
                              }
                            ]} />
                        </div>
                      </div>
                    </Card>
                  ))
                }
              </div>
            </React.Fragment>
          )
        }
      </div>
    </React.Fragment>
  );

};

export default React.memo(MainPage);
