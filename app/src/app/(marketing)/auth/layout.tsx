export default function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="flex justify-center">
      <div className="lg:w-1/4">{children}</div>
    </div>
  );
}
