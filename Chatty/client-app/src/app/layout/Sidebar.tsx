import { Button, Container, Text, VStack } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import ChatRoomList from "../../features/chat/roomList/ChatRoomList";



export default observer(function Sidebar() {

    return (

        <Container
            variant='main-style'
            m='5' mt='4%' p='2' pos='fixed' left='0'
            maxWidth='15vw'
            borderRadius='1rem'
        >
            <VStack
                height='60vh'
            >
                <Text
                    fontSize='lg' noOfLines={1}
                    maxWidth='14vw' p='1' as='b'
                >
                    Select channel:
                </Text>
                <ChatRoomList />
                <Button 
                    width='100%' 
                    variant='main-style'
                    as={Link} to='/chat'
                >
                    Back to panel
                </Button>
            </VStack>
        </Container>
    )
})