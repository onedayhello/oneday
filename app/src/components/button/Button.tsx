import { ReactNode } from "react";

export const Button = ({
  children,
  variant,
}: {
  children: ReactNode;
  variant?: string;
}) => {
  const color = (() => {
    switch (variant) {
      case "primary":
        return "bg-[#00A870]";
      case "secondary":
        return "bg-neutral-400";
      default:
        return "bg-[#00A870]";
    }
  })();

  return (
    <button
    className={` hover:contrast-125 shadow flex items-center py-2  tracking-wide font-serif font-bold  rounded px-4 text-white active:shadow-none ${color}`}
    >
      {children}
    </button>
  );
};
