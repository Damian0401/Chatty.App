import { VStack, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { useEffect } from "react";
import { useStore } from "../../../app/stores/store";
import ChatRoomListItem from "./ChatRoomListItem"


export default observer(function ChatRoomList() {

    const { chatStore: { roomRegistry, createHubConnection, stopHubConnection } } = useStore();

    useEffect(() => {
        createHubConnection();

        return () => stopHubConnection();
    }, [])

    return (
        <VStack
            bgColor='whiteAlpha.100' p='2'
            borderRadius='1rem' gap='0'
            width='100%' height='100%'
            overflowY="auto" overflowX='hidden'
            fontFamily='sans-serif'
        >
            {roomRegistry.size > 0
                ? Array.from(roomRegistry.values()).map(room => (
                    <ChatRoomListItem room={room} key={room.id} />
                )) : <Text>There are no rooms yet.</Text>}
        </VStack>
    )
})