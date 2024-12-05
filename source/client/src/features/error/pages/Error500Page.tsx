//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import Presenter from './Error500Page.presenter';

interface Error500PageProps {
  error?: Error
}

function Error500Page(props: Readonly<Error500PageProps>) {

  const { error } = props;

  React.useEffect(() => {
    if (error == null) {
      return;
    }
  }, [
    error
  ]);

  return (
    <Presenter error={error?.message} />
  );

}

export default Error500Page;
