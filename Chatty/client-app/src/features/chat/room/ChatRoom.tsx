import { Box, Button, Center, Container, Flex, Input } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useStore } from "../../../app/stores/store";
import ChatRoomHeader from "./ChatRoomHeader";
import ChatRoomMessage from "./ChatRoomMessage";


export default observer(function ChatRoom() {
    const { chatStore } = useStore();

    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        chatStore.selectRoom(id);
    }, [id]);

    return (
        <Center>
            <Container
                m='3' p='0'
                variant='main-style'
                height='70vh'
                pos='relative'
            >
                <ChatRoomHeader />
                <Flex flexDir='column' height='100%'>
                    <Flex flexDir='column-reverse' height='100%' width='100%' overflow='auto' p='2' pb='0'>
                            {chatStore.selectedRoom && chatStore.selectedRoom.messages?.map((message) => (
                                <>
                                <ChatRoomMessage message={message} key={message.id} />
                                </>
                            ))}
                    </Flex>
                    <Flex width='100%'>
                        <Input borderRadius='0 0 0 1rem' border='none' bgColor='whiteAlpha.300' />
                        <Button
                            variant='main-style'
                            bgColor='blue.600'
                            borderRadius='0 0 1rem 0'
                        >
                            Send
                        </Button>
                    </Flex>
                </Flex>
            </Container>
        </Center>
    )
})