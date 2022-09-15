import { Box, Flex, Spacer, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { Link } from "react-router-dom"


export default observer(function ChatRoomListItem() {


    return (
        <Box
            bgColor='whiteAlpha.200'
            p='1' maxWidth='12vw'
            as={Link} to='/chat/5'
            borderRadius='0.2rem'
            _hover={{bgColor: 'whiteAlpha.300'}}
        >
            <Flex>
                <Text noOfLines={1} as='b'>
                    {'{channelName}'}:
                </Text>
                <Spacer />
            </Flex>
            <Text noOfLines={1}>{'{userName}'}: This is very long last message</Text>
        </Box>
    )
})