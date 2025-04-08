import React from 'react';
import { Outlet, Link } from 'react-router-dom';
import { Box, Drawer, List, ListItem, ListItemText, Typography } from '@mui/material';
import { styled } from '@mui/system';

const Sidebar = styled(Drawer)({
  width: 250,
  '& .MuiDrawer-paper': {
    width: 250,
    background: 'linear-gradient(180deg, #1A2C34 0%, #0F1C22 100%)',
    color: '#E0E0E0',
    borderRight: '1px solid #00D4FF',
    boxShadow: '0 0 15px rgba(0, 212, 255, 0.4)',
  },
});

const Content = styled(Box)({
  flexGrow: 1,
  padding: '30px',
  backgroundColor: '#0F1C22',
  minHeight: '100vh',
});

const HeisenbergLink = styled(Link)({
  textDecoration: 'none',
  color: '#00D4FF',
  fontWeight: 'bold',
  padding: '10px 20px',
  borderRadius: '8px',
  '&:hover': { backgroundColor: '#1E2F38', color: '#FFFFFF' },
});

function AdminLayout() {
  return (
    <Box sx={{ display: 'flex' }}>
      <Sidebar variant="permanent" anchor="left">
        <Typography
          variant="h5"
          sx={{ p: 3, color: '#00D4FF', textAlign: 'center', fontWeight: 'bold' }}
        >
          [He][Br] Admin
        </Typography>
        <List>
          <ListItem button component={HeisenbergLink} to="/admin/dashboard">
            <ListItemText primary="[Ta]lepler" />
          </ListItem>
          <ListItem button component={HeisenbergLink} to="/admin/add-project">
            <ListItemText primary="[Pr]oje Ekle" />
          </ListItem>
        </List>
      </Sidebar>
      <Content>
        <Outlet />
      </Content>
    </Box>
  );
}

export default AdminLayout;