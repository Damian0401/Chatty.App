import { DeleteIcon } from "@chakra-ui/icons";
import { Box, Button, Flex, IconButton, Popover, PopoverBody, PopoverContent, PopoverTrigger, Text, Tooltip } from "@chakra-ui/react";
import { formatDistanceToNow } from "date-fns";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Message } from "../../../app/models/message";
import { useStore } from "../../../app/stores/store";

interface Props {
    message: Message;
}

export default observer(function ChatRoomMessage({ message }: Props) {

    const { userStore: { user }, chatStore: { selectedRoom, isRoomAdministrator, deleteMessage } } = useStore();

    const [isAuthor, setIsAuthor] = useState(false);

    const [isVisible, setIsVisible] = useState(false);

    useEffect(() => {
        setIsAuthor(message.authorId === user?.id);
    }, []);

    return (
        <Box
            onMouseEnter={() => setIsVisible(true)}
            onMouseLeave={() => setIsVisible(false)}
        >
            <Tooltip hasArrow label={formatDistanceToNow(message.createdAt) + ' ago'}>
                <Box
                    p='2' m='1'
                    width='fit-content'
                    bgColor={isAuthor ? 'blue.600' : 'gray.500'}
                    float={isAuthor ? 'right' : 'left'}
                    borderRadius='1rem'
                    maxWidth='100%'
                    boxShadow='xl'
                >
                    {!isAuthor && <Text as='b' noOfLines={1}>
                        {selectedRoom?.users?.find(x => x.id === message.authorId)?.displayName || '{userName}'}:
                    </Text>}
                    <Text>
                        {message.body}
                    </Text>
                </Box>
            </Tooltip>
            {isVisible && (isAuthor || isRoomAdministrator) &&
                <Flex float={isAuthor ? 'right' : 'left'} alignItems='center' height='100%'>
                    <IconButton
                        aria-label='delete message'
                        size='xs' bgColor='none'
                        variant='ghost' icon={<DeleteIcon />}
                        onClick={() => deleteMessage(message.id)}
                    />
                </Flex>}
        </Box>
    )
})