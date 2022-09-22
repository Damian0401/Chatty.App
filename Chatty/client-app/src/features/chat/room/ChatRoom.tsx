import { Button, Center, Container, Flex, Input, InputGroup, InputRightElement } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import { useStore } from "../../../app/stores/store";
import ChatRoomHeader from "./ChatRoomHeader";
import ChatMessageContainer from "../message/ChatMessageContainer";
import { toast } from "react-toastify";
import Picker, { IEmojiData } from 'emoji-picker-react';


export default observer(function ChatRoom() {
    const { chatStore } = useStore();

    const { id } = useParams<{ id: string }>();

    const [messageBody, setMessageBody] = useState('');

    const [isPickerVisible, setIsPickerVisible] = useState(false);
    
    const bottomRef = useRef<HTMLDivElement | null>(null);

    useEffect(() => {
        chatStore.selectRoom(id);
    }, [id]);

    useEffect(() => {
        bottomRef.current?.scrollIntoView({ behavior: 'smooth' });
    }, [chatStore.selectedRoom?.messages?.length]);

    const handleSendClick = () => {
        if (messageBody === '') {
            toast.warning('You must provide a message body.');
            return;
        }

        chatStore.sendMessage(messageBody);
        setMessageBody('');
    }

    const handleEmojiClick = (data: IEmojiData) => {
        setMessageBody(messageBody + data.emoji);
        setIsPickerVisible(false);
    }

    return (
        <Center>
            <Container
                m='3' p='0'
                variant='main-style'
                height='70vh'
                pos='relative'
                maxWidth='40vw'
            >
                <ChatRoomHeader />
                <Flex flexDir='column' height='100%'>
                    <Flex
                        flexDir='column-reverse'
                        height='100%' width='100%'
                        overflow='auto' p='2' pb='1'
                        onClick={() => setIsPickerVisible(false)}
                    >
                        <div ref={bottomRef} />
                        {chatStore.selectedRoom && chatStore.selectedRoom.messages?.map((message) => (
                            <ChatMessageContainer message={message} key={message.id} />
                        ))}
                    </Flex>
                    <Flex width='100%'>
                        <InputGroup pos='relative'>
                            <Input
                                borderRadius='0 0 0 1rem'
                                value={messageBody}
                                border='none'
                                bgColor='whiteAlpha.300'
                                onChange={e => setMessageBody(e.target.value)}
                                onClick={() => setIsPickerVisible(false)}
                            />
                            <InputRightElement children={
                                <>
                                    <Button
                                        bgColor='transparent'
                                        _hover={{ bgColor: 'whiteAlpha.200' }}
                                        onClick={() => setIsPickerVisible(true)}
                                    >
                                        🙂
                                    </Button>
                                    {isPickerVisible &&
                                        <Picker
                                            disableSearchBar
                                            onEmojiClick={(_, d) => handleEmojiClick(d)}
                                            pickerStyle={{ position: 'absolute', bottom: '0' }}
                                        />}
                                </>
                            } />
                        </InputGroup>
                        <Button
                            variant='main-style'
                            bgColor='blue.600'
                            borderRadius='0 0 1rem 0'
                            onClick={handleSendClick}
                        >
                            Send
                        </Button>
                    </Flex>
                </Flex>
            </Container>
        </Center>
    )
})