import React, { useState } from 'react';
import axios from 'axios';
import {
  Container,
  Typography,
  TextField,
  Button,
  Box,
  CircularProgress,
} from '@mui/material';

function AddProject({ token }) {
  const [newProject, setNewProject] = useState({ name: '', description: '' });
  const [loading, setLoading] = useState(false);

  const handleCreateProject = async () => {
    setLoading(true);
    try {
      const response = await axios.post('https://localhost:7271/api/Project', newProject, {
        headers: { Authorization: `Bearer ${token}` },
      });
      console.log('Yeni proje ID:', response.data);
      setNewProject({ name: '', description: '' });
      alert('Proje oluşturuldu!');
    } catch (error) {
      console.error('Proje oluşturma hatası:', error.response ? error.response.data : error.message);
      alert('Proje oluşturulurken hata oluştu!');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" sx={{ mb: 3, textAlign: 'center' }}>
        [Pr]oje Ekle
      </Typography>
      <Box sx={{ p: 3, bgcolor: '#1E2F38', borderRadius: 2, boxShadow: '0 0 15px rgba(0, 212, 255, 0.3)' }}>
        <TextField
          label="Proje Adı"
          value={newProject.name}
          onChange={(e) => setNewProject({ ...newProject, name: e.target.value })}
          fullWidth
          margin="normal"
          variant="outlined"
          sx={{
            input: { color: '#E0E0E0' },
            '& .MuiOutlinedInput-root': {
              '& fieldset': { borderColor: '#00D4FF' },
              '&:hover fieldset': { borderColor: '#FFFFFF' },
              '&.Mui-focused fieldset': { borderColor: '#00D4FF' },
            },
            '& .MuiInputLabel-root': { color: '#00D4FF' },
          }}
        />
        <TextField
          label="Açıklama"
          value={newProject.description}
          onChange={(e) => setNewProject({ ...newProject, description: e.target.value })}
          fullWidth
          margin="normal"
          multiline
          rows={3}
          variant="outlined"
          sx={{
            input: { color: '#E0E0E0' },
            '& .MuiOutlinedInput-root': {
              '& fieldset': { borderColor: '#00D4FF' },
              '&:hover fieldset': { borderColor: '#FFFFFF' },
              '&.Mui-focused fieldset': { borderColor: '#00D4FF' },
            },
            '& .MuiInputLabel-root': { color: '#00D4FF' },
          }}
        />
        <Button
          variant="contained"
          onClick={handleCreateProject}
          fullWidth
          disabled={loading || !newProject.name}
          sx={{ mt: 2 }}
        >
          {loading ? <CircularProgress size={24} sx={{ color: '#1A2C34' }} /> : '[+] Proje'}
        </Button>
      </Box>
    </Container>
  );
}

export default AddProject;