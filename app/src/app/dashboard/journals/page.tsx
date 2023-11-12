const RecentJournalsItem = () => {
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

const Journal = () => {
  return (
    <div className="flex flex-col gap-3">
      <div>
        <h1 className="text-2xl">Journals</h1>
        <hr />
      </div>

      <div className="flex mt-4 gap-2">
        <div className="p-2 px-4 border rounded-full">New Entry</div>
        <div className="bg-[#454545] text-white p-2 px-4  rounded-full">
          Past Entries
        </div>
      </div>

      <div className="flex flex-col gap-2">
        <RecentJournalsItem />
        <RecentJournalsItem />
        <RecentJournalsItem />
      </div>
    </div>
  );
};

export default Journal;
