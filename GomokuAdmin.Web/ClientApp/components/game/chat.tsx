import * as React from "react";
import { IChatModel } from "@Models/IChatModel";
import { IGameModel } from "@Models/IGameModel";
import { Formik, Field } from 'formik';
import FormValidator from "@Components/shared/FormValidator";
import { GUID } from "../../models/GuidType";
import GameService from '@Services/GameService';
import { ServiceBase } from "../../core/ServiceBase";
import Result from "../../core/Result";
import { wait } from "domain-wait";




//async function getChat(props: GUID) {
//    var gameService = new GameService();
//    return gameService.getChat(props).then(function (result) {
//        return result.value;
//    });
//}



interface IProps {
    id: GUID;
}

export const ChatComponent: React.FC<IProps> = ({ id}: IProps) => {
    const [error, setError] = React.useState(null);
    const [isLoaded, setIsLoaded] = React.useState(false);
    const [items, setItems] = React.useState([] as IChatModel[]);


    var api = "api/Game/GetChat?id=" + id;
    React.useEffect(() => {
        fetch(api)
            .then(res => res.json())
            .then(
                (result) => {
                    setIsLoaded(true);
                    setItems(result.value);
                },
                // Note: it's important to handle errors here
                // instead of a catch() block so that we don't swallow
                // exceptions from actual bugs in components.
                (error) => {
                    setIsLoaded(true);
                    setError(error);
                }
            )
    }, [])

    if (error) {
        return <div>Error: {error.message}</div>;
    } else if (!isLoaded) {
        return <div>Loading...</div>;
    } else {
        return (
            <ul>
                {items.map(item => (
                    <li>
                        {item.name}:  {item.content}
                    </li>
                ))}
            </ul>
        );
    }
}