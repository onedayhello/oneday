'use client'

import Question from './Question';
import questions from '@/data/questions';
import { useState, useEffect } from 'react'

const Questionnaire = () => {
    const questionsAtATime = 5;

    const [count, setCount] = useState<number>(0);
    const [scores, setScores] = useState<number[]>([0,0,0,0,0]);
    const [total, setTotal] = useState<number>(0);
    const [show, setShow] = useState<boolean>(false);

    const increment = () => {
        const subTotal = scores.reduce((acc, current) => acc + current, 0);

        setTotal(total + subTotal);
        setScores([0,0,0,0,0]);

        if (count < 4) {
            setCount(count + 1);
        } else {
            setShow(true);
        }
    };

    const updateScores = (index: number, score: number) => {
        const updatedScores = [...scores];
        updatedScores[index] = score;
        setScores(updatedScores);
    }

    useEffect(() => {
        if (scores.every((val) => val > 0)) {
            increment();
        }
    },[scores])

    return (
        <>
            <h1 className="text-4xl mb-2">How are you feeling right now?</h1>
            <hr />
            <div className="grid lg:grid-cols-3 gap-12 mt-6">
                {show ? 
                <div className="flex items-center justify-center col-span-2">
                    <h1>your Burn's depression index score is: {total}</h1>
                </div> :
                <div className="flex flex-col gap-4 col-span-2">
                    <div className="grid grid-cols-3">
                        <span className="col-span-2 text-2xl font-thin">
                        Burn's Depression Index
                        </span>
                        <div className="flex gap-2 mt-auto mb-1">
                        <span className="text-center w-8">1</span>
                        <span className="text-center w-8">2</span>
                        <span className="text-center w-8">3</span>
                        <span className="text-center w-8">4</span>
                        <span className="text-center w-8">5</span>
                        </div>
                    </div>
                    {questions.slice(count*questionsAtATime, count*questionsAtATime+questionsAtATime).map((q, i) => (
                        <Question question={q} index={i} update={updateScores} key={q}/>
                    ))}
                </div>}

                <div className="text-content text-xl leading-7 font-extralight">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
                enim ad minim veniam, quis nostrud exercitation ullamco laboris
                nisi ut aliquip ex ea commodo consequat. Sed ut perspiciatis unde
                omnis iste natus error sit voluptatem accusantium doloremque
                laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore
                veritatis et quasi architecto beatae vitae dicta sunt explicabo
                </div>
            </div>
        </>
    )
}

export default Questionnaire;