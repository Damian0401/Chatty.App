import { VStack } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import ChatRoomListItem from "./ChatRoomListItem"


export default observer(function ChatRoomList() {


    return (
        <VStack
            bgColor='whiteAlpha.100' p='2'
            borderRadius='1rem' gap='0'
            overflowY="auto" overflowX='hidden'
            fontFamily='sans-serif'
        >
            <ChatRoomListItem />
            <ChatRoomListItem />
            <ChatRoomListItem />
        </VStack>
    )
})