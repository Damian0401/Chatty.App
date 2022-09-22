import { VStack, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { useEffect } from "react";
import { useStore } from "../../../app/stores/store";
import ChatRoomListItem from "./ChatRoomListItem"


export default observer(function ChatRoomList() {

    const { chatStore: { sortedRooms, createHubConnection, stopHubConnection } } = useStore();

    useEffect(() => {
        createHubConnection();

        return () => stopHubConnection();
    }, [createHubConnection, stopHubConnection])

    return (
        <VStack
            bgColor='whiteAlpha.300' p='2'
            borderRadius='1rem' gap='0'
            width='100%' height='100%'
            overflowY="auto" overflowX='hidden'
            fontFamily='sans-serif'
        >
            {sortedRooms.length > 0
                ? sortedRooms.map(room => (
                    <ChatRoomListItem room={room} key={room.id} />
                )) : <Text>There are no rooms yet.</Text>}
        </VStack>
    )
})