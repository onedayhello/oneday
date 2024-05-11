import type { Meta, StoryObj } from "@storybook/react";

import { TextInput } from "../components/input";

const meta: Meta<typeof TextInput> = {
  component: TextInput,
};

export default meta;
type Story = StoryObj<typeof TextInput>;

export const Primary: Story = {
  render: () => <TextInput />,
};
