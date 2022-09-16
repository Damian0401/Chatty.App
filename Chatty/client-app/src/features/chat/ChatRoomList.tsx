import { VStack } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { useEffect } from "react";
import { useStore } from "../../app/stores/store";
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
            overflowY="auto" overflowX='hidden'
            fontFamily='sans-serif'
        >
            {Array.from(roomRegistry.values()).map(room => (
                <ChatRoomListItem room={room} key={room.id} />
            ))}
        </VStack>
    )
})