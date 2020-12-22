import axios from "axios";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";

interface ICardSet {
    id: number,
    year: number,
    brand: string,
    description: string,
    name: string
}

interface ICheckList {
    collectionId: number,
    sets: ICardSet[];
}

const CardChecklist = () => {
    const api: string = 'https://localhost:44329/api/cards/setchecklists/1';

    const [allSets, setAllSets] = useState<ICardSet[]>([]);

    useEffect(() => {
        axios.get<ICheckList>(api).then(response => {
            setAllSets(response.data.sets);
            })
        }, []);

    return (
        <div >
            <div>
                filters
            </div>
            <ul>
                Card Sets:
                {allSets.length == 0 &&
                    <div>Loading</div>
                }
                {allSets &&                  
                    allSets.map((set) => (
                        <div key={set.id} >
                            {set.name}
                        </div>
                    ))                  
                }
            </ul>
        </div>
    );
}

export default CardChecklist;