import { Box, Flex, Spacer, Text, Tooltip } from "@chakra-ui/react"
import { formatDistanceToNow } from "date-fns";
import { observer } from "mobx-react-lite"
import { Link } from "react-router-dom"
import { Room } from "../../../app/models/room"

interface Props {
    room: Room;
}

export default observer(function ChatRoomListItem({ room }: Props) {


    return (
        <Box
            bgColor='whiteAlpha.200'
            as={Link} to={`/chat/${room.id}`}
            borderRadius='0.2rem'
            width='100%' p='1'
            _hover={{ bgColor: 'whiteAlpha.300' }}
        >
            <Flex>
                <Text noOfLines={1} as='b'>
                    {room.name || '{channelName}'}:
                </Text>
                <Spacer />
            </Flex>
            {room.messages && room.messages.length > 0 &&
            <Tooltip hasArrow label={formatDistanceToNow(room.messages[0].createdAt) + ' ago'}>
                <Text noOfLines={1}>{room.messages[0].body}</Text>
            </Tooltip>
            }
        </Box>
    )
})