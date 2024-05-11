import type { Preview } from '@storybook/react';

import { withThemeByClassName } from '@storybook/addon-styling';

/* TODO: update import to your tailwind styles file */
import '../src/app/globals.css';

import { Roboto, Alegreya } from "next/font/google";
import React from 'react';

const inter = Alegreya({ subsets: ["latin"], variable: "--font-alegreya" });
const roboto = Roboto({
  subsets: ["latin"],
  weight: ["300", "400", "500", "700"],
  variable: "--font-roboto",
});

const preview: Preview = {
  decorators: [
    (Story) => (
      <div className={`${inter.variable} ${roboto.variable}`}>
        <Story />
      </ div>
    ),
  ],
};

export default preview;
