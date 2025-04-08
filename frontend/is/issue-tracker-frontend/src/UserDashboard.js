import React from 'react';
import { Container, Typography, Box, Paper, Fade } from '@mui/material';
import IssueForm from './IssueForm';

function UserDashboard({ token, showForm }) {
  return (
    <Container maxWidth="md" sx={{ mt: 5, mb: 5 }}>
      <Fade in={true} timeout={500}>
        <Box sx={{ mb: 4 }}>
          <Typography
            variant="h4"
            component="h1"
            sx={{ fontWeight: 'bold', color: 'primary.main' }}
          >
            Kullanıcı Paneli
          </Typography>
        </Box>
      </Fade>

      {showForm && (
        <Fade in={showForm} timeout={700}>
          <Paper
            elevation={3}
            sx={{
              p: 3,
              borderRadius: 3,
              bgcolor: 'background.paper',
              boxShadow: '0 4px 20px rgba(0, 0, 0, 0.05)',
            }}
          >
            <Typography variant="h6" sx={{ mb: 2, color: 'text.primary' }}>
              Yeni Destek Talebi
            </Typography>
            <IssueForm token={token} />
          </Paper>
        </Fade>
      )}
    </Container>
  );
}

export default UserDashboard;