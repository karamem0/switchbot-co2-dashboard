//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

interface MsalInteractionGateProps {
  loading?: boolean
}

function MsalInteractionGate(props: Readonly<React.PropsWithChildren<MsalInteractionGateProps>>) {

  const {
    children,
    loading
  } = props;

  return loading ? null : children;

}

export default React.memo(MsalInteractionGate);
