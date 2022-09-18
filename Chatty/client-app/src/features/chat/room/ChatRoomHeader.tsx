import { ArrowBackIcon, ChevronDownIcon, DeleteIcon, EditIcon, HamburgerIcon, LinkIcon, SettingsIcon } from "@chakra-ui/icons";
import { Box, Button, Container, Flex, Icon, Menu, MenuButton, MenuItem, MenuList, Popover, PopoverArrow, PopoverBody, PopoverCloseButton, PopoverContent, PopoverTrigger, Spacer, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { toast } from "react-toastify";
import { store, useStore } from "../../../app/stores/store";



export default observer(function ChatRoomHeader() {

    const { chatStore, userStore } = useStore();

    const handleInvitationClick = () => {
        if (!chatStore.selectedRoom) return;

        navigator.clipboard.writeText(chatStore.selectedRoom.id);
        toast.success('Copied invitation code to the clipboard');
    }

    return (
        <Container pos='absolute' top='0' p='0' maxWidth='100%'>
            <Flex>
                <Menu>
                    <MenuButton
                        as={Button}
                        rightIcon={<HamburgerIcon />}
                        aria-label='Options'
                        borderRadius='1rem 0 1rem 0'
                        variant='main-style'
                    >
                        {store.chatStore.selectedRoom?.name || '{roomName}'}
                    </MenuButton>
                    <MenuList>
                        <MenuItem icon={<EditIcon />}>
                            Change display name
                        </MenuItem>
                        <MenuItem icon={<LinkIcon />} onClick={handleInvitationClick}>
                            Copy invitation code 
                        </MenuItem>
                        <MenuItem icon={<ArrowBackIcon />}>
                            Exit room
                        </MenuItem>
                    </MenuList>
                </Menu>
                <Spacer />
                    <Popover placement='bottom-end'>
                        <PopoverTrigger>
                            <Button
                                leftIcon={<ChevronDownIcon />}
                                variant='main-style'
                                borderRadius='0 1rem 0 1rem'
                            >
                                Users ({chatStore.selectedRoom?.users?.length})
                            </Button>
                        </PopoverTrigger>
                        <PopoverContent>
                            <PopoverArrow />
                            <PopoverCloseButton />
                            <PopoverBody>
                                {chatStore.selectedRoom?.users?.map(user => (
                                    <Box key={user.id}>
                                        {user.displayName} {user.id === userStore.user?.id && '(You) '}
                                        {chatStore.isRoomAdministrator && <DeleteIcon />}
                                    </Box>
                                ))}
                            </PopoverBody>
                        </PopoverContent>
                    </Popover>
            </Flex>
        </Container>
    )
})