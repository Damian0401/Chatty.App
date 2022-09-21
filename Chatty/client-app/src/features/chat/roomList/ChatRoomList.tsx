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
    }, [createHubConnection, stopHubConnection])

    return (
        <VStack
            bgColor='whiteAlpha.100' p='2'
            borderRadius='1rem' gap='0'
            width='100%' height='100%'
            overflowY="auto" overflowX='hidden'
            fontFamily='sans-serif'
        >
            {roomRegistry.size > 0
                ? Array.from(roomRegistry.values()).sort((a, b) => {
                    if (!a.messages || a.messages.length === 0) return 1;
                    if (!b.messages || b.messages.length === 0) return -1;
                    return b.messages[0].createdAt.getTime() - a.messages[0].createdAt.getTime();
                }).map(room => (
                    <ChatRoomListItem room={room} key={room.id} />
                )) : <Text>There are no rooms yet.</Text>}
        </VStack>
    )
})