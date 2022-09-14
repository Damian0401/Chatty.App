import { Text, VStack } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import ChatRoomList from "../../features/chat/ChatRoomList";



export default observer(function Sidebar() {

    return (
        <VStack
            bgColor='whiteAlpha.300'
            m='5' p='2' pos='fixed' left='0'
            height='60vh'
            maxWidth='15vw'
            borderRadius='1rem'
            color='blackAlpha.700'
        >
            <Text
                fontSize='lg' noOfLines={1}
                maxWidth='14vw' p='1' as='b'
            >
                Select channel:
            </Text>
            <ChatRoomList />
        </VStack>
    )
})