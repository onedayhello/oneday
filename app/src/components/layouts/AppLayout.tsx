import Navbar from "../navigation/Navbar";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div>
      <Navbar />
      {children}
      <footer className="mt-20 flex justify-between">
        <div className="flex flex-col gap-2 font-medium capitalize tracking-wider">
          <div className="  ">PRIVACY & LEGAL</div>
          <div className="">CONTACT</div>
        </div>
        <div>© 2023 - One Day, All rights reserved.</div>
      </footer>
    </div>
  );
}
