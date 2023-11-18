import { ReactNode } from "react";

const Layout = ({ children }: { children: ReactNode }) => {
  return (
    <div className="">
      <div className="mb-4">
        <h1 className="text-2xl font-bold">Journals</h1>
        <hr />
      </div>

      <div>{children}</div>
    </div>
  );
};

export default Layout;
