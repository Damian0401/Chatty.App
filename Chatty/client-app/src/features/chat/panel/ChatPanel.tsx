import { Button, Center, Container, Flex, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import InputModal from "../../../app/common/modals/InputModal";
import { useStore } from "../../../app/stores/store";


export default observer(function ChatPanel() {

    const { userStore: { user }, modalStore: { openModal }, chatStore: {joinRoom, createRoom} } = useStore();

    const handleJoinClick = () => {
        openModal(<InputModal 
            description='Enter invitation code of the room that you want to join:'
            buttonText='Join'
            buttonColor='blue.600'
            handleSubmit={(value) => joinRoom(value)}
        />)
    }

    const handleCreateClick = () => {
        openModal(<InputModal 
            description='Enter the name of room that you want to create:'
            buttonText='Create'
            buttonColor='green.600'
            handleSubmit={(value) => createRoom(value)}
        />)
    }

    return (
        <Center>
            <Container
                p='8' m='3'
                variant='main-style'
                maxWidth='40vw'
                height='70vh'
            >
                <Flex flexDirection='column' height='100%' justifyContent='space-between'>
                    <Text fontSize='2xl' noOfLines={3}>
                        Welcome to the chat control panel, <strong>{user ? user.userName : '{userName}'}</strong>.
                        Here you can create a new chat room or join to existing one.
                    </Text>
                    <Flex justifyContent='space-between'>
                        <Button variant='main-style' size='lg' onClick={handleJoinClick}>
                            Join
                        </Button>
                        <Button variant='main-style' size='lg' onClick={handleCreateClick}>
                            Create
                        </Button>
                    </Flex>
                </Flex>
            </Container>
        </Center>
    )
})