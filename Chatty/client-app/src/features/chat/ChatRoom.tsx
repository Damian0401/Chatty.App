import { Center } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";


export default observer(function ChatRoom() {
    const { id } = useParams<{id: string}>();

    return (
        <Center>
            <h1>ChatRoom, id: {id}</h1>
        </Center>
    )
})