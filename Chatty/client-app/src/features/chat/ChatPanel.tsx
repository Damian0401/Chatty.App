import { Button, Center, Container, Flex, FormControl, FormLabel, Input, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { useState } from "react";
import { useStore } from "../../app/stores/store";



export default observer(function ChatPanel() {

    const { userStore: { user } } = useStore();

    const [isInputVisible, setIsInputVisible] = useState(false);
    const [action, setAction] = useState('');

    const handleJoinClick = () => {
        setAction('id');
        setIsInputVisible(!isInputVisible);
    }

    const handleCreateClick = () => {
        setAction('name');
        setIsInputVisible(!isInputVisible);
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
                    {isInputVisible &&
                        <FormControl isRequired>
                            <FormLabel htmlFor='panel-input'>
                                Enter {action} of the room:
                            </FormLabel>
                            <Input id='panel-input' m='1' />
                            <Button variant='main-style' m='1' float='right' size='sm'>
                                Submit
                            </Button>
                        </FormControl>}
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