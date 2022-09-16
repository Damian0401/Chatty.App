import { Box, Flex, Spacer, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { Link } from "react-router-dom"
import { Room } from "../../app/models/room"

interface Props {
    room: Room;
}

export default observer(function ChatRoomListItem({ room }: Props) {


    return (
        <Box
            bgColor='whiteAlpha.200'
            p='1' maxWidth='12vw'
            as={Link} to={`/chat/${room.id}`}
            borderRadius='0.2rem'
            _hover={{ bgColor: 'whiteAlpha.300' }}
        >
            <Flex>
                <Text noOfLines={1} as='b'>
                    {room.name || '{channelName}'}:
                </Text>
                <Spacer />
            </Flex>
            {room.messages && room.messages.length > 0 &&
                <Text noOfLines={1}>{room.messages[0].body}</Text>
            }
        </Box>
    )
})