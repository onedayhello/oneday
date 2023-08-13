import { Dropdown, TextInput } from "@/components/input";
import Questionnaire from "../../components/questionnaire/Questionnaire";

export default function Home() {
  return (
    <main className="mb-16">
      <section className="grid lg:grid-cols-7 gap-14">
        <div className="flex flex-col lg:col-span-3 bg-[#384B42] p-12 rounded-2xl">
          <h1 className="text-4xl lg:text-6xl leading-normal tracking-wide font-bold text-white mb-6">
            Your depression companion
          </h1>
          <span className="text-white/80">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
            ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
            aliquip ex ea commodo consequat.
          </span>
          <div className="ml-auto text-xl mt-12 bg-[#00A870]  inline-block py-4 px-8 text-white font-serif font-bold rounded">
            Create an account
          </div>
        </div>
        <div className="border p-8 lg:col-span-4 rounded-2xl">
          <h1 className="text-4xl lg:text-6xl leading-normal tracking-wide font-bold mb-6 text-gray-800">
            How does One Day help you?
          </h1>
          <div className="mb-4">
            <h2 className="text-xl mb-2 font-bold text-gray-800">
              Identify and replace depressive thought patterns using Mindfulness
              Journal
            </h2>
            <span className="text-gray-600">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam, quis nostrud exercitation ullamco laboris
              nisi ut aliquip ex ea commodo consequat.
            </span>
          </div>

          <div>
            <h2 className="mb-2 text-xl font-bold text-gray-800">
              Track your mood over time
            </h2>
            <span className="text-gray-600">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam, quis nostrud exercitation ullamco laboris
              nisi ut aliquip ex ea commodo consequat.
            </span>
          </div>
        </div>
      </section>

      <section className="my-24">
        <Questionnaire />
      </section>

      <section>
        <div className="flex gap-4 mb-4">
          <div className="inline-block bg-[#454545] text-white px-4 py-2 rounded-full">
            Mindfulness Journal
          </div>
          <div className="inline-block border px-4 py-2 rounded-full">
            Mindfulness Journal
          </div>
        </div>

        <div className="rounded-2xl lg:p-20 grid lg:grid-cols-8 gap-12 bg-[#F8F9F8]">
          <div className="lg:col-span-3">
            <h2 className="text-4xl mb-2">Mindfulness Journal for you</h2>
            <div>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam, quis nostrud exercitation ullamco laboris
              nisi ut aliquip ex ea commodo consequat.
            </div>
          </div>
          <div className="gap-1 lg:col-span-5">
            <div className="grid grid-cols-3 mb-2 gap-4">
              <span>Thought</span> <span>Type of thinking</span>
              <span>Better thought</span>
            </div>
            <hr className="mb-2" />
            <div className="flex flex-col gap-2">
              <div className="relative grid lg:grid-cols-3 gap-4 items-top">
                <div className="absolute self-center -left-5">-</div>
                <TextInput />
                <Dropdown />
                <TextInput />
              </div>
              <div className="relative grid lg:grid-cols-3 gap-4 items-top">
                <div className="absolute self-center -left-5">+</div>
                <TextInput />
                <Dropdown />
                <TextInput />
              </div>
            </div>
          </div>
        </div>
      </section>
    </main>
  );
}
