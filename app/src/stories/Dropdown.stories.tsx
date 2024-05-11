import type { Meta, StoryObj } from "@storybook/react";

import { Dropdown } from "../components/input";

const meta: Meta<typeof Dropdown> = {
  component: Dropdown,
};

export default meta;
type Story = StoryObj<typeof Dropdown>;

export const Primary: Story = {
  render: () => <Dropdown />,
};
