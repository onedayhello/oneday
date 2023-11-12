"use client";

import { ReactElement, ReactNode } from "react";
import Graph from "./Graph";
import Link from "next/link";
import Auth from "@/util/Auth";
import { useRouter } from "next/navigation";

const Subpanel = ({
  title,
  subtitle,
  button,
  children,
}: {
  title: ReactNode;
  subtitle?: ReactNode;
  button?: ReactNode;
  children?: ReactNode;
}) => {
  return (
    <div className="">
      <div className="mb-2">
        <span className="flex justify-between">
          <h2 className="text-2xl font-bold">{title}</h2>
          <button className="text-sm text-[#6C6C6C]">{button}</button>
        </span>
        <h3 className="text-[#6C6C6C] text-sm">{subtitle}</h3>
      </div>
      {children}
    </div>
  );
};

const SubpanelYourTasksItem = () => {
  return (
    <div className="flex justify-between rounded-lg p-4 items-center hover:bg-[#F4F4F3]">
      <div>
        <h3 className="font-bold text-lg">Mindfulness Journal</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
      </div>
      <div>+</div>
    </div>
  );
};

const SubpanelRecentJournalsItem = () => {
  return (
    <div className="flex  justify-between rounded-lg p-4 hover:bg-[#F4F4F3] border">
      <div>
        <div className="flex gap-1 mb-2">
          <p className="border rounded-full px-3 text-sm text-[#6C6C6C]">CBT</p>
          <p className="border rounded-full px-3 text-sm text-[#6C6C6C]">
            Anxiety
          </p>
        </div>
        <h3 className="font-bold text-lg">Mindfulness Journal</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
      </div>
      <div className="text-sm text-[#C6C6C6]">5d</div>
    </div>
  );
};

const App = () => {
  const router = useRouter();

  return (
    <>
      <div className="flex flex-col gap-6">
        <Subpanel title="Your tasks" subtitle="Tasks to complete today">
          <SubpanelYourTasksItem></SubpanelYourTasksItem>
          <SubpanelYourTasksItem></SubpanelYourTasksItem>
        </Subpanel>
        <Subpanel
          title="Mood tracker"
          subtitle="Track your mood with questionnaires"
          button="Month"
        >
          <Graph />
        </Subpanel>
      </div>
      <div>
        <Subpanel
          title="Recently journals"
          subtitle="Journals to help improve mindfulness"
          button="View all"
        >
          <div className="flex flex-col gap-3">
            <SubpanelRecentJournalsItem></SubpanelRecentJournalsItem>
            <SubpanelRecentJournalsItem></SubpanelRecentJournalsItem>
            <SubpanelRecentJournalsItem></SubpanelRecentJournalsItem>
          </div>
        </Subpanel>
      </div>
    </>
  );
};

export default App;
