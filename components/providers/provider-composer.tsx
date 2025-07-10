import React, { ReactNode } from "react";

type ProviderProps = {
  children: ReactNode;
  [key: string]: any;
};

type ProviderTuple = [React.ComponentType<any>, Record<string, any>?];

type ProviderComposerProps = {
  providers: ProviderTuple[];
  children: ReactNode;
};

export default function ProviderComposer({
  providers,
  children,
}: ProviderComposerProps) {
  return providers.reduceRight((kids, [Provider, props = {}]) => {
    return <Provider {...props}>{kids}</Provider>;
  }, children);
}
